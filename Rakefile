require 'net/http'

SERVER_PID = './server.pid'

def kill_process(pid)
    Process.kill("INT", pid.to_i)
end

def remove_pid_file
    File.delete(SERVER_PID) if File.exist? SERVER_PID
end

task :stop_server do
    return unless File.exist? SERVER_PID
    pid = File.read SERVER_PID
    kill_process pid
    remove_pid_file
end

task :clean do
    FileUtils.rm_rf('bin')
    FileUtils.rm_rf('obj')
    remove_pid_file
end

task :restore do
    sh "dotnet restore"
end

task :start do
    pid = Process.spawn('dotnet', 'run')
    Process.detach pid
end

task :ping do
    begin
        retries ||= 0
        uri = URI('http://localhost:5000')
        res = Net::HTTP.get_response(uri)
        fail "request failed with code #{res.code}" unless res.code == '200'
        fail 'api is not ready' unless res.body.include? '"status": true'
        puts 'API IS RUNNING'
    rescue Exception => e 
        if (retries += 1) < 10
            sleep 1000
            retry 
        else
            fail "api request failed ---> #{e.message}"
        end
    end
end

task default: [:clean, :restore, :start]