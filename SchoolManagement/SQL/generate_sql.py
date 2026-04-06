import csv
import datetime
import re

# =============================================================================
# Configuration
# =============================================================================
INPUT_FILE = r'C:\Users\kakan\Documents\GitHub\NeuroPi\SchoolManagement\SQL\student_data.txt'
OUTPUT_FILE = r'C:\Users\kakan\Documents\GitHub\NeuroPi\SchoolManagement\SQL\student_registration_import.sql'

# Mappings (Normalized keys to IDs)
BRANCH_MAP = {
    'miyapur': 17,
    'manikonda': 15,
    'kukatpally': 14,
    'kavuri hills': 12,
    'kondapur': 13,
    'commissioner office': 11,
    'cyberabad police commissionerate': 11,
}

COURSE_MAP = {
    'pre-nursery': 2,
    'pre nursery': 2,
    'nursery': 1,
    'ciao baby': 7,
    'day care': 5,
    'dc': 5,
    'kindergarten 2': 4,
    'k2': 4,
    'kindergarten 1': 3,
    'k1': 3,
    'grade 1': 6,
    'g1': 6,
    'grade 2': 8,
    'g2': 8,
}

def clean_str(s):
    if not s: return ''
    return s.strip()

def clean_email(s):
    if not s: return None
    cleaned = s.replace(' ', '').strip()
    if not cleaned or cleaned.lower() == 'na': return None
    return cleaned

def clean_phone(s):
    if not s: return None
    cleaned = re.sub(r'[\s\-]', '', s)
    if not cleaned or len(cleaned) < 10: return None
    return cleaned

def parse_date(date_str):
    if not date_str: return None
    date_str = clean_str(date_str)
    for fmt in ('%d/%m/%Y', '%d-%m-%Y', '%Y-%m-%d'):
        try:
            return datetime.datetime.strptime(date_str, fmt).strftime('%Y-%m-%d')
        except ValueError:
            continue
    return None

def generate_sql():
    with open(INPUT_FILE, 'r', encoding='utf-8') as f:
        csv_reader = csv.reader(f, delimiter='\t')
        rows = list(csv_reader)

    if rows and 'Student First Name' in rows[0][0]:
        rows = rows[1:]

    sql_content = []
    
    sql_content.append("-- Student Registration Script - Generated Version")
    sql_content.append(f"-- Generated on {datetime.datetime.now().strftime('%Y-%m-%d %H:%M:%S')}")
    sql_content.append("""
DO $$
DECLARE
    v_tenant_id INT := 1;
    v_parent_role_id INT := 5;
    v_password TEXT := 'changeme';
    v_created_by INT := 1;
    v_placeholder_phone TEXT := '0000000000';
    v_placeholder_addr TEXT := 'NA';
    v_placeholder_city TEXT := 'Hyderabad';
    v_placeholder_state TEXT := 'Telangana';
    v_placeholder_pincode TEXT := '000000';
    v_f_contact_id INT;
    v_m_contact_id INT;
    v_p_user_id INT; -- Primary User ID
    v_p_parent_id INT; -- Primary Parent ID
    v_student_id INT;
    v_branch_id INT;
    v_course_id INT;
    v_p_name TEXT;
    v_p_email TEXT;
    v_p_phone TEXT;
    v_p_type TEXT; 
    v_p_uname TEXT;
    v_p_fname TEXT;
    v_p_lname TEXT;
    v_p_email_to_use TEXT;
    v_p_dob DATE;
BEGIN
""")

    count = 0
    for row in rows:
        if not row or not any(row): continue
        if len(row) < 23: row += [''] * (23 - len(row))

        count += 1
        
        # Student
        s_fname, s_mname, s_lname = clean_str(row[0]), clean_str(row[1]), clean_str(row[2])
        s_dob = parse_date(row[3])
        s_gender = clean_str(row[4])
        s_branch_raw = clean_str(row[5])
        s_reg_id = clean_str(row[6])
        s_class_raw = clean_str(row[7])
        s_blood = clean_str(row[21])
        s_doj = parse_date(row[22])

        branch_id = BRANCH_MAP.get(s_branch_raw.lower(), 'NULL')
        course_id = COURSE_MAP.get(s_class_raw.lower(), 'NULL')

        # Father
        f_fname, f_mname, f_lname = clean_str(row[8]), clean_str(row[9]), clean_str(row[10])
        f_dob = parse_date(row[11])
        f_email = clean_email(row[12])
        f_phone = clean_phone(row[13])
        f_full_name = f"{f_fname} {f_mname} {f_lname}".replace('  ', ' ').strip()
        f_uname = f_email if f_email else (f"{f_fname.lower()}_{f_phone}" if f_phone else f"user_{count}_f")

        # Mother
        m_fname, m_mname, m_lname = clean_str(row[14]), clean_str(row[15]), clean_str(row[16])
        m_dob = parse_date(row[17])
        m_phone = clean_phone(row[18])
        m_email = clean_email(row[19])
        m_full_name = f"{m_fname} {m_mname} {m_lname}".replace('  ', ' ').strip()
        m_uname = m_email if m_email else (f"{m_fname.lower()}_{m_phone}" if m_phone else f"user_{count}_m")

        sql_content.append(f"\n-- =============================================================================")
        sql_content.append(f"-- STUDENT {count}: {s_fname} {s_lname}")
        sql_content.append(f"-- =============================================================================")
        
        # ------------------------------------------------------------------
        # FATHER CONTACT
        # ------------------------------------------------------------------
        sql_content.append(f"-- Father Contact: {f_full_name}")
        sql_content.append(f"v_p_name := '{f_full_name}'; v_p_email := {repr(f_email) if f_email else 'NULL'}; v_p_phone := {repr(f_phone) if f_phone else 'v_placeholder_phone'}; v_p_type := 'FATHER';")
        
        sql_content.append("""
    IF v_p_email IS NOT NULL THEN
        SELECT id INTO v_f_contact_id FROM public.contact WHERE email = v_p_email AND name = v_p_name AND tenant_id = v_tenant_id LIMIT 1;
    ELSE
        SELECT id INTO v_f_contact_id FROM public.contact WHERE pri_number = v_p_phone AND name = v_p_name AND tenant_id = v_tenant_id LIMIT 1;
    END IF;
    
    IF v_f_contact_id IS NULL THEN
        INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode, created_by)
        VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode, v_created_by) RETURNING id INTO v_f_contact_id;
    END IF;
""")

        # ------------------------------------------------------------------
        # MOTHER CONTACT
        # ------------------------------------------------------------------
        sql_content.append(f"-- Mother Contact: {m_full_name}")
        sql_content.append(f"v_p_name := '{m_full_name}'; v_p_email := {repr(m_email) if m_email else 'NULL'}; v_p_phone := {repr(m_phone) if m_phone else 'v_placeholder_phone'}; v_p_type := 'MOTHER';")

        sql_content.append("""
    IF v_p_email IS NOT NULL THEN
        SELECT id INTO v_m_contact_id FROM public.contact WHERE email = v_p_email AND name = v_p_name AND tenant_id = v_tenant_id LIMIT 1;
    ELSE
        SELECT id INTO v_m_contact_id FROM public.contact WHERE pri_number = v_p_phone AND name = v_p_name AND tenant_id = v_tenant_id LIMIT 1;
    END IF;

    IF v_m_contact_id IS NULL THEN
        INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode, created_by)
        VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode, v_created_by) RETURNING id INTO v_m_contact_id;
    END IF;
""")

        # ------------------------------------------------------------------
        # LOGIN CREATION (Per Kid One Login Only)
        # ------------------------------------------------------------------
        # Logic: Prefer Father. If Father exists, create User/Parent for Father.
        # If Father missing details, try Mother.
        # Here we assume Father is primary if name exists.
        
        is_father_primary = True if f_fname else False
        
        if is_father_primary:
             sql_content.append(f"-- Primary Login Identity: FATHER ({f_full_name})")
             sql_content.append(f"v_p_uname := '{f_uname}'; v_p_fname := '{f_fname}'; v_p_lname := '{f_lname}';")
             sql_content.append(f"v_p_email := {repr(f_email) if f_email else 'NULL'}; v_p_phone := {repr(f_phone) if f_phone else 'v_placeholder_phone'};")
             sql_content.append(f"v_p_dob := {repr(f_dob) if f_dob else 'NULL'};")
        else:
             sql_content.append(f"-- Primary Login Identity: MOTHER ({m_full_name})")
             sql_content.append(f"v_p_uname := '{m_uname}'; v_p_fname := '{m_fname}'; v_p_lname := '{m_lname}';")
             sql_content.append(f"v_p_email := {repr(m_email) if m_email else 'NULL'}; v_p_phone := {repr(m_phone) if m_phone else 'v_placeholder_phone'};")
             sql_content.append(f"v_p_dob := {repr(m_dob) if m_dob else 'NULL'};")
        
        sql_content.append("""
    -- User & Role (Primary)
    v_p_user_id := NULL;
    IF v_p_email IS NOT NULL THEN
        SELECT user_id INTO v_p_user_id FROM public.users WHERE email = v_p_email AND tenant_id = v_tenant_id LIMIT 1;
    END IF;
    IF v_p_user_id IS NULL THEN
         SELECT user_id INTO v_p_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id LIMIT 1;
    END IF;

    IF v_p_user_id IS NULL THEN
        v_p_email_to_use := COALESCE(v_p_email, v_p_uname || '@neuropi.placeholder');
        INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, mobile_number, dob, created_by)
        VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email_to_use, v_tenant_id, v_p_phone, v_p_dob, v_created_by) RETURNING user_id INTO v_p_user_id;

        INSERT INTO public.user_roles (user_id, role_id, tenant_id, created_by) VALUES (v_p_user_id, v_parent_role_id, v_tenant_id, v_created_by);
    END IF;

    -- Parent (Primary)
    SELECT id INTO v_p_parent_id FROM public.parents WHERE user_id = v_p_user_id AND tenant_id = v_tenant_id LIMIT 1;
    IF v_p_parent_id IS NULL THEN
        INSERT INTO public.parents (user_id, tenant_id, created_by) VALUES (v_p_user_id, v_tenant_id, v_created_by) RETURNING id INTO v_p_parent_id;
    END IF;
""")

        # Student
        val_s_dob = repr(s_dob) if s_dob else 'NULL'
        val_s_doj = repr(s_doj) if s_doj else 'NULL'
        val_s_reg = repr(s_reg_id) if s_reg_id else 'NULL'
        val_s_blood = repr(s_blood) if s_blood else 'NULL'

        sql_content.append(f"""
    -- Student: {s_fname}
    SELECT id INTO v_student_id FROM public.students WHERE first_name = '{s_fname}' AND last_name = '{s_lname}' AND dob = {val_s_dob} AND tenant_id = v_tenant_id LIMIT 1;
    
    IF v_student_id IS NULL THEN
        INSERT INTO public.students (first_name, middle_name, last_name, dob, gender, branch_id, course_id, tenant_id, reg_number, f_contact, m_contact, bloodgroup, date_of_joining, created_by)
        VALUES ('{s_fname}', '{s_mname}', '{s_lname}', {val_s_dob}, '{s_gender}', {branch_id}, {course_id}, v_tenant_id, {val_s_reg}, v_f_contact_id, v_m_contact_id, {val_s_blood}, {val_s_doj}, v_created_by) RETURNING id INTO v_student_id;
        
        -- Map Parents (Primary Only)
        IF NOT EXISTS (SELECT 1 FROM public.parent_student WHERE parent_id = v_p_parent_id AND student_id = v_student_id AND tenant_id = v_tenant_id) THEN
            INSERT INTO public.parent_student (parent_id, student_id, tenant_id, created_by) VALUES (v_p_parent_id, v_student_id, v_tenant_id, v_created_by);
        END IF;

        INSERT INTO public.student_course (student_id, course_id, branch_id, tenant_id, is_current_year, created_by) VALUES (v_student_id, {course_id}, {branch_id}, v_tenant_id, TRUE, v_created_by);
    END IF;
""")

    sql_content.append("END $$;")

    with open(OUTPUT_FILE, 'w', encoding='utf-8') as f:
        f.write('\n'.join(sql_content))
    print(f"Successfully generated SQL with {count} students.")

if __name__ == "__main__":
    generate_sql()
